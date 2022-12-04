struct ButtonState {

	bool pressed;

};

enum buttonData {

	BUTTON_UP,
	BUTTON_DOWN,
	BUTTON_LEFT,
	BUTTON_RIGHT,

	BUTTON_COUNT,
};

enum entity {

	EMPTY,
	PLAYER_HEAD,
	PLAYER_NECK,
	PLAYER_TAIL,
	FOOD,

	ENTITY_COUNT,

};

enum movement {

	UP,
	DOWN,
	LEFT,
	RIGHT,

};

struct Input {

	ButtonState buttons[BUTTON_COUNT];

};

struct Snake {

	int x, y;

};