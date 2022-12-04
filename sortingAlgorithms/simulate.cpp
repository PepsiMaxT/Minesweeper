bool a = false;

internal int
simulate(Data* data, float dt) {

	clearScreen(0xff5555);

	for (int i = 0; i < 192; i++) {

		draw(data->dataMeta[i].position, data->dataMeta[i].position + 10, 10, data->dataMeta[i].value + 10, 0xffffff);


	}

	return 0;
}