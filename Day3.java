import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Scanner;

public class Day3 {

	static File input;
	static ArrayList<String> lines;
	static ArrayList<int[]> triangleDims;
	static ArrayList<int[]> triangleDims2;
	
	public static void main(String[] args) {
		input = new File("input3.txt");
		triangleDims = new ArrayList<int[]>();
		triangleDims2 = new ArrayList<int[]>();
		lines = new ArrayList<String>();
		
		try {
			Scanner sc = new Scanner(input);
			while (sc.hasNextLine()) {
				lines.add(sc.nextLine());
				}
			for (String line : lines) {
				String[] sA = line.trim().split("\\s+");
				int[] iA = new int[sA.length];
				for (int i = 0; i < sA.length; i++) {
					iA[i] = Integer.parseInt(sA[i]);
					}
				triangleDims.add(iA);
			}
		}
		catch (FileNotFoundException e) {
			e.printStackTrace();
		}
		
		//Part One
		int count = 0;
		for (int[] triangle : triangleDims) {
			if (isTriangle(triangle)) {
				count++;
			}
		}
		System.out.println("Part One: " + Integer.toString(count));
		
		
		//Part Two
		for (int i = 0; i < triangleDims.size(); i += 3) {
			int[] iA0 = {triangleDims.get(i)[0], 
					triangleDims.get(i+1)[0], triangleDims.get(i+2)[0]};
			int[] iA1 = {triangleDims.get(i)[1], 
					triangleDims.get(i+1)[1], triangleDims.get(i+2)[1]};
			int[] iA2 = {triangleDims.get(i)[2], 
					triangleDims.get(i+1)[2], triangleDims.get(i+2)[2]};
			triangleDims2.add(iA0);
			triangleDims2.add(iA1);
			triangleDims2.add(iA2);
		}
		
		count = 0;
		for (int[] triangle : triangleDims2) {
			if (isTriangle(triangle)) {
				count++;
			}
		}
		System.out.println("Part Two: " + Integer.toString(count));
		
	}

	//Part One and Two
	public static boolean isTriangle(int[] triangle) {
		if (triangle[0] >= triangle[1] + triangle[2]) {
			return false;
		}
		if (triangle[1] >= triangle[0] + triangle[2]) {
			return false;
		}
		if (triangle[2] >= triangle[0] + triangle[1]) {
			return false;
		}
		return true;
	}
	
	
	
}
