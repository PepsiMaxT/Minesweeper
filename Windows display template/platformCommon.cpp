struct ButtonState {

	bool isDown;
	bool changed;

};

enum buttonData {

	BUTTON_COUNT,
};

struct Input {

	ButtonState buttons[BUTTON_COUNT];

};

