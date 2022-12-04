#include "utils.cpp"
#include <windows.h>

globalVariable bool running = 1;
globalVariable bool heightChangeInitialised = 0;
globalVariable bool widthChangeInitialised = 0;

globalVariable float prevTime = 0;
globalVariable float time = 0;

globalVariable int playSpace[24][18] = {};
globalVariable int direction = 3;

globalVariable int bodyLength = 2;

//Used for establishing the window size and where in memory it is stored
struct RenderState {

	int height, width;
	float sizeChangeWidth, sizeChangeHeight;
	void* memory;

	BITMAPINFO bitmapInfo;
};

globalVariable RenderState renderState;

//Include other files, order is important
#include "platformCommon.cpp"
#include "renderer.cpp"
#include "simulate.cpp"

//Receives instructions from the window about the window
LRESULT CALLBACK windowCallback(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {

	LRESULT result = 0;

	//Deciding the outcome dependant on the message
	switch (uMsg) {
		case WM_CLOSE:
		case WM_DESTROY: {

			running = false;

		} break;

		case WM_SIZE: {

			//Gets information about the window, e.g dimensions


			RECT rect;
			GetClientRect(hwnd, &rect);

			//Establish heights, width, size of window.
			renderState.width = rect.right - rect.left;
			renderState.height = rect.bottom - rect.top;

			renderState.sizeChangeWidth = float(renderState.width) / 480.0f;
			renderState.sizeChangeHeight = float(renderState.height) / 360.0f;

			int size = renderState.width * renderState.height * sizeof(u32);

			//Clearing the memory of current size, then allocating the new memory
			if (renderState.memory) VirtualFree(renderState.memory, 0, MEM_RELEASE);
			renderState.memory = VirtualAlloc(0, size, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);

			//Establish data about the bitmap
			renderState.bitmapInfo.bmiHeader.biSize = sizeof(renderState.bitmapInfo.bmiHeader);
			renderState.bitmapInfo.bmiHeader.biWidth = renderState.width;
			renderState.bitmapInfo.bmiHeader.biHeight = renderState.height;
			renderState.bitmapInfo.bmiHeader.biPlanes = 1;
			renderState.bitmapInfo.bmiHeader.biBitCount = 32;
			renderState.bitmapInfo.bmiHeader.biCompression = BI_RGB;

		}
		
		default: {

			result = DefWindowProc(hwnd, uMsg, wParam, lParam);

		}

	}
	return result;

}

//Start the window and main code
int WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nShowCmd) {

	//ShowCursor(FALSE);

	//Create Window Class
	WNDCLASS windowClass = {};
	windowClass.style = CS_HREDRAW | CS_VREDRAW;
	windowClass.lpszClassName = L"Game Window Class";
	windowClass.lpfnWndProc = windowCallback;

	

	//Register Class
	RegisterClass(&windowClass);

	//Create Window
	HWND window = CreateWindow(windowClass.lpszClassName, L"Game Name Here (platformer)", WS_OVERLAPPEDWINDOW | WS_VISIBLE, CW_USEDEFAULT, CW_USEDEFAULT, 496, 399, 0, 0, hInstance, 0);
	HDC hdc = GetDC(window);

	//From structure Input to gather key presses
	Input input = {};

	//Start establishing time to make movement consistent across FPS
	float deltaTime = 0.166666;
	LARGE_INTEGER frameBeginTime;
	QueryPerformanceCounter(&frameBeginTime);

	float performanceFrequency; {

		LARGE_INTEGER perf;
		QueryPerformanceFrequency(&perf);
		performanceFrequency = (float)perf.QuadPart;

	}

	//RUN ONCE
	srand((0));
	
	globalVariable Snake snakeTiles[432];
	snakeTiles[0].x = 10;
	snakeTiles[0].y = 9;
	snakeTiles[1].x = 11;
	snakeTiles[1].y = 9;
	direction = RIGHT;

	while (running) {

		//Input
		MSG message;

		//Recieves a message from the window, links back to the start of code, also for all key presses
		while (PeekMessage(&message, window, 0, 0, PM_REMOVE)) {

			//Which message to execute
			switch (message.message) {

				case WM_KEYUP:
				case WM_KEYDOWN: {

					u32 vk_code = (u32)message.wParam;
					bool isDown = ((message.lParam & (1 << 31)) == 0);

					//Define to shorten button presses and performance
#define processButton(b, vk)\
case vk: {\
input.buttons[b].pressed = true; \
} break; \

					switch (vk_code) {

						//process_button(<button>, <VK BUTTON>);
						processButton(BUTTON_UP, 'W');
						processButton(BUTTON_UP, VK_UP);
						processButton(BUTTON_DOWN, 'S');
						processButton(BUTTON_DOWN, VK_DOWN);
						processButton(BUTTON_LEFT, 'A');
						processButton(BUTTON_LEFT, VK_LEFT);
						processButton(BUTTON_RIGHT, 'D');
						processButton(BUTTON_RIGHT, VK_RIGHT);


					}

				} break;

				default: {

					//Message understanding
					TranslateMessage(&message);
					DispatchMessage(&message);

				}

			}

		}

		//Simulate
		if (!simulate(&input, roundDown(time), roundDown(prevTime))) running = 0;

		//Render
		StretchDIBits(hdc, 0, 0, renderState.width, renderState.height, 0, 0, renderState.width, renderState.height, renderState.memory, &renderState.bitmapInfo, DIB_RGB_COLORS, SRCCOPY);

		//Finish delta time
		LARGE_INTEGER frameEndTime;
		QueryPerformanceCounter(&frameEndTime);
		prevTime = time;
		deltaTime = (float)(frameEndTime.QuadPart - frameBeginTime.QuadPart) / performanceFrequency;
		time += deltaTime;

		frameBeginTime = frameEndTime;

	}

}