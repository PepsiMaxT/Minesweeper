internal void
clearScreen(u32 colour) {

	u32* pixel = (u32*)renderState.memory;
	for (int y = 0; y < renderState.height; y++) {
		for (int x = 0; x < renderState.width; x++) {

			*pixel++ = colour;

		}

	}

}

internal void
drawPlayer(float x0, float x1, float y0, float y1, u32 colour) {

	int cameray0 = scale(camera.y0 * renderState.sizeChangeHeight, 1);
	int cameray1 = scale(camera.y1 * renderState.sizeChangeHeight, 1);

	x0 = scale(x0 * renderState.sizeChangeWidth, 1);
	x1 = scale(x1 * renderState.sizeChangeWidth, 1);
	y0 = scale(y0 * renderState.sizeChangeHeight, 1);
	y1 = scale(y1 * renderState.sizeChangeHeight, 1);

	for (int y = y0 - cameray0; y < y1 - cameray0; y++) {
		u32* pixel = (u32*)renderState.memory + int(x0) + y * renderState.width;
		for (int x = x0; x < x1; x++) {

			if (y != y1) *pixel++ = colour;
		}
	}
}

internal void
draw(float x0,float x1, float y0, float y1, u32 colour) {
	
	int cameray0 = scale(0 * renderState.sizeChangeHeight, 1);
	int cameray1 = scale(681 * renderState.sizeChangeHeight, 1);

	x0 = scale(x0 * renderState.sizeChangeWidth, 1);
	x1 = scale(x1 * renderState.sizeChangeWidth, 1);
	y0 = scale(y0 * renderState.sizeChangeHeight, 1);
	y1 = scale(y1 * renderState.sizeChangeHeight, 1);

	if (!((y1 < cameray0) || (y0 > cameray1))) {
		for (int y = y0 - cameray0; y < y1 - cameray0; y++) {
			if (y < 0) y = 0;
			if (y > renderState.height - 1) y = y1;
			u32* pixel = (u32*)renderState.memory + int(x0) + y * renderState.width;
			for (int x = x0; x < x1; x++) {

				if (y != y1) *pixel++ = colour;
			}
		}
	}
}