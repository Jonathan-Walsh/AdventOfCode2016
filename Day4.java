import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Scanner;

public class Day4 {

	public static void main(String[] args) {
		File input = new File("input4.txt");
		ArrayList<String> lines = new ArrayList<String>();	
		try {
			Scanner sc = new Scanner(input);
			while (sc.hasNextLine()) {
				lines.add(sc.nextLine());
				}
		}
		catch (FileNotFoundException e) {
			e.printStackTrace();
		}
		
		//Part One and Two
		int sum = 0;
		for (String line : lines) {
			sum += isRealRoom(line);
			decode(line);
		}
		System.out.println("Part One: " + Integer.toString(sum));

	}
	
	//Part One
	public static int isRealRoom(String line) {
		String[] codes = line.split("-");
		int[] freqs = new int[26];
	//Update freqs array
		for (int i = 0; i < codes.length - 1; i++) {
			String code = codes[i];
			for (int j = 0; j < code.length(); j++) {
				int letterVal = Character.getNumericValue(code.charAt(j)) - 10;
				freqs[letterVal] += 1;
			}
		}
	//Determine isRealRoom
		int[] highestFreqs = new int[5];
		for (int i = 0; i < 5; i++) {
			int max = 0;
			int max_val = freqs[0];
			for (int j = 0; j < freqs.length; j++) {
				if (freqs[j] > max_val) {
					max = j;
					max_val = freqs[j];
				}
			}
			highestFreqs[i] = max;
			freqs[max] = -1;
		}
		
		boolean[] match = new boolean[5];			//All five must become true
		int[] iA = new int[5];
		for (int i = 4; i < 9; i++) {
			int letterVal = Character.getNumericValue(codes[codes.length - 1].charAt(i)) - 10;
			iA[i-4] = letterVal;
			for (int j = 0; j < highestFreqs.length; j++) {
				if (letterVal == highestFreqs[j]) {
					match[j] = true;
					break;
				}
			}
		}
		
		for (int i = 0; i < match.length; i++) {
			if (match[i] == false) {
				return 0;
			}
		}
		
		//System.out.println(Arrays.toString(highestFreqs) + " " + Arrays.toString(iA));
		return Integer.parseInt(codes[codes.length - 1].substring(0, 3));
	}
	
	//Part Two
	public static void decode(String line) {
		String[] codes = line.split("-");
		int[] freqs = new int[26];
	//Update freqs array
		for (int i = 0; i < codes.length - 1; i++) {
			String code = codes[i];
			char[] codeChars = code.toCharArray();
			for (int j = 0; j < codeChars.length; j++) {
				int letterVal = Character.getNumericValue(code.charAt(j)) - 10;
				letterVal += Integer.parseInt(codes[codes.length - 1].substring(0, 3));
				letterVal %= 26;
				letterVal += 97;
				char c = (char) letterVal;
				codeChars[j] = c;
			}
			String s = "";
			for (char c: codeChars) {
				s += c;
			}
			codes[i] = s;
		}
		
		System.out.println(Arrays.toString(codes));
		//TO KNOW: Look for "northpole" and you will find the correct room
	}
	

}
