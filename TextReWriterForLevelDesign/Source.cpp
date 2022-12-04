#include <iostream>
#include <string>

int main() {
	std::string text;
	std::string newText;

	int speechCount = 0;
	std::cin >> text;
	for (int x = 0; x < sizeof(text); x++) {

		if (text[x] == (char)"\"") speechCount++;
		else newText += text[x];

		if (speechCount == 2) {
			speechCount = 0;
			std::cout << newText;
			newText = "";
			std::cout << std::endl;
		}
	}
}