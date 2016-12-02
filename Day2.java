import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Scanner;

public class Day2 {
	
	static File input;
	static ArrayList<String> lines = new ArrayList<String>();
	static int currentNum;
	static char currentChar;
	
	public static void main(String[] args) {
		input = new File("input2.txt");
		
		try {
			Scanner sc = new Scanner(input);
			while (sc.hasNextLine()) {
				lines.add(sc.nextLine());
			}
		
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		}
		
		//Part One
		currentNum = 5;
		System.out.print("Part One: ");
		for (String line : lines) {
			currentNum = findNextNum(line, currentNum);
			System.out.print(currentNum);
		}
		System.out.println("");
		
		//Part Two
		currentChar = '5';
		System.out.print("Part Two: ");
		for (String line : lines) {
			currentChar = findNextKey(line, currentChar);
			System.out.print(currentChar);
		}
		System.out.println("");
	}
	
	static int[][] keyPad = {{1,2,3},{4,5,6},{7,8,9}};
	
	//Part One
	public static int findNextNum(String line, int currentNum) {
		int x = 0;
		int y = 0;
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (keyPad[i][j] == currentNum) {
					x = j;
					y = i;
				}
			}
		}
		
		for (int k = 0; k < line.length(); k++) {
			char c = line.charAt(k);
			if (c == 'D' && y < 2) {
				y += 1;
				
			}
			else if (c == 'U' && y > 0) {
				y -= 1;
			}
			else if (c == 'L' && x > 0) {
				x -= 1;
			}
			else if (c == 'R' && x < 2) {
				x += 1;
			}
			currentNum = keyPad[y][x];
		}
		return currentNum;
	}

	
	static char[][] keyPad2 = {{0,0,'1',0,0},{0,'2','3','4',0},{'5','6','7','8','9'},
			{0,'A','B','C',0},{0,0,'D',0,0}};
	
	//Part Two
	public static char findNextKey(String line, char currentChar) {
		int x = 0;
		int y = 0;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				if (keyPad2[i][j] == currentChar) {
					x = j;
					y = i;
				}
			}
		}
		
		for (int k = 0; k < line.length(); k++) {
			char c = line.charAt(k);
			if (c == 'D' && y < 4 && keyPad2[y+1][x] != 0) {
				y += 1;
			}
			else if (c == 'U' && y > 0 && keyPad2[y-1][x] != 0) {
				y -= 1;
			}
			else if (c == 'L' && x > 0 && keyPad2[y][x-1] != 0) {
				x -= 1;
			}
			else if (c == 'R' && x < 4 && keyPad2[y][x+1] != 0) {
				x += 1;
			}
			currentChar = keyPad2[y][x];
		}
		
		return currentChar;
	}
	
}
