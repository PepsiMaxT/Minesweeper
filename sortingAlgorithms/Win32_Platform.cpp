#include <iostream>
#include <stdlib.h>
#include "utils.cpp"
#include <windows.h>

//Determines the games lifetime
globalVariable float gravity = 650;
globalVariable bool running = 1;
bool heightChangeInitialised = 0;
bool widthChangeInitialised = 0;
bool positionValid = false;

//Determine player initial location according to level
int level = 3;

//Used for establishing the window size and where in memory it is stored
struct RenderState {

	int height, width;
	float sizeChangeWidth, sizeChangeHeight;
	void* memory;

	BITMAPINFO bitmapInfo;
};

globalVariable RenderState renderState;

//Camera structure
struct Camera {

	float y0, y1;
	int maxY;

};

globalVariable Camera camera;

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

			renderState.sizeChangeWidth = 0.01 * (float(renderState.width) / 19.24);
			renderState.sizeChangeHeight = 0.01 * (float(renderState.height) / 6.81);

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

	bool occupied[193] = {};

	srand(time(0));

	Data data = {};



	for (int i = 0; i < 192; i += 1) {

		data.dataMeta[i].value = i;
		while (!positionValid) {

			//data.dataMeta[i].position = (rand() % 192) * 10;
			int a = (rand() % 192);
			data.dataMeta[i].position = a * 10;

			if (!occupied[a]) { positionValid = true; occupied[a] = true; }

		}
		positionValid = false;



	}


	//ShowCursor(FALSE);

	//Create Window Class
	WNDCLASS windowClass = {};
	windowClass.style = CS_HREDRAW | CS_VREDRAW;
	windowClass.lpszClassName = L"Game Window Class";
	windowClass.lpfnWndProc = windowCallback;

	

	//Register Class
	RegisterClass(&windowClass);

	//Create Window
	HWND window = CreateWindow(windowClass.lpszClassName, L"Game Name Here (platformer)", WS_OVERLAPPEDWINDOW | WS_VISIBLE, CW_USEDEFAULT, CW_USEDEFAULT, 2000, 720, 0, 0, hInstance, 0);
	HDC hdc = GetDC(window);

	//From structure Input to gather key presses

	//Start establishing time to make movement consistent across FPS
	float deltaTime = 0.166666;
	LARGE_INTEGER frameBeginTime;
	QueryPerformanceCounter(&frameBeginTime);

	float performanceFrequency; {

		LARGE_INTEGER perf;
		QueryPerformanceFrequency(&perf);
		performanceFrequency = (float)perf.QuadPart;

	}

	while (running) {

		//Input
		MSG message;

		//Check for button state chenge, press -> release, release -> press
		/*for (int i = 0; i < BUTTON_COUNT; i++) {

			input.buttons[i].changed = false;

		}*/

		//Recieves a message from the window, links back to the start of code, also for all key presses
		while (PeekMessage(&message, window, 0, 0, PM_REMOVE)) {

			/*//Which message to execute
			switch (message.message) {

				case WM_KEYUP:
				case WM_KEYDOWN: {

					u32 vk_code = (u32)message.wParam;
					bool isDown = ((message.lParam & (1 << 31)) == 0);

					//Define to shorten button presses and performance
#define processButton(b, vk)\
case vk: {\
input.buttons[b].isDown = isDown; \
input.buttons[b].changed = true; \
} break; \

					switch (vk_code) {

						//process_button(<button>, <VK BUTTON>);
						processButton(BUTTON_RIGHT, 'D');
						processButton(BUTTON_LEFT, 'A');
						processButton(BUTTON_UP, 'W');
						processButton(BUTTON_DOWN, 'S');
						processButton(BUTTON_JUMP, VK_SPACE);
						processButton(BUTTON_CROUCH, VK_SHIFT);

					}

				} break; */

				//default: {

					//Message understanding
					TranslateMessage(&message);
					DispatchMessage(&message);

				//}


		}

		//Simulate
		simulate(&data, deltaTime);

		//Render
		StretchDIBits(hdc, 0, 0, renderState.width, renderState.height, 0, 0, renderState.width, renderState.height, renderState.memory, &renderState.bitmapInfo, DIB_RGB_COLORS, SRCCOPY);

		//Finish delta time
		LARGE_INTEGER frameEndTime;
		QueryPerformanceCounter(&frameEndTime);
		deltaTime = (float)(frameEndTime.QuadPart - frameBeginTime.QuadPart) / performanceFrequency;
		frameBeginTime = frameEndTime;

	}

}