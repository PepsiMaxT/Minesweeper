#define pressed(b) (input->buttons[b].pressed)

int i, j, x, y;

internal int
simulate(Input* input, int time, int prevTime, Snake* snakeTiles) {

	int lastSnakeX = snakeTiles[bodyLength - 1].x;
	int lastSnakeY = snakeTiles[bodyLength - 1].y;

	for (i = 0; i < 4; i++) {
		if (pressed(i)) {
			direction = i;
		}
	}
	if (time > prevTime) {

		for (i = bodyLength - 1; i > 0; i--) {
			snakeTiles[i].x = snakeTiles[x - 1].x;
			snakeTiles[i].y = snakeTiles[x - 1].y;
		}
	}
}