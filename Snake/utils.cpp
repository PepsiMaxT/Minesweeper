typedef char s8;
typedef unsigned char u8;
typedef short s16;
typedef unsigned short u16;
typedef int s32;
typedef unsigned int u32;
typedef long long s64;
typedef unsigned long long u64;

#define globalVariable static
#define internal static

float round(float roundNum, float targetVal) {

	float num = 0;
	for (roundNum += (0.5 * targetVal); roundNum > targetVal; roundNum -= targetVal) {
		roundNum += targetVal;
	}

	return num;

}

int roundDown(float roundNum) {

	int num = 0;
	
	for (roundNum = roundNum; roundNum > 0; roundNum--) {
		num++;
	}

	return num;

}

float scale(float roundNum, float targetVal) {

	float num = 0;
	for (roundNum += (0.5 * targetVal); roundNum > targetVal; roundNum -= targetVal) {
		num += targetVal;
	}

	return num;

}