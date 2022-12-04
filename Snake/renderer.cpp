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
draw(float x0,float x1, float y0, float y1, u32 colour) {

	x0 = scale(x0 * renderState.sizeChangeWidth, 1);
	x1 = scale(x1 * renderState.sizeChangeWidth, 1);
	y0 = scale(y0 * renderState.sizeChangeHeight, 1);
	y1 = scale(y1 * renderState.sizeChangeHeight, 1);

	for (int y = y0; y < y1; y++) {
		u32* pixel = (u32*)renderState.memory + int(x0) + y * renderState.width;
		for (int x = x0; x < x1; x++) {
			if (y != y1) *pixel++ = colour;
		}
	}
}