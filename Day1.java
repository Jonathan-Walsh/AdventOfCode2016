import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Scanner;

public class Day1 {

	static String[] directions;
	static File input;
	static String[] cardinalDirections = {"up", "left", "down", "right"};
	
	public static void main(String[] args) {;
		input = new File("input1.txt");
	
		try {
			Scanner sc = new Scanner(input);
			String line = sc.nextLine();
			directions = line.toString().split(", ");
		
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		}
		
		int distance1 = findDistance(directions);
		int distance2 = findFirstRevisitedLocation(directions);
		System.out.println("Part One: " + Integer.toString(distance1));
		System.out.println("Part Two: " + Integer.toString(distance2));
	}

	//Part One
	//Returns distance of the final location
	public static int findDistance(String[] directions) {
		int direction = 0;	//Start up
		String currentDir = cardinalDirections[direction];
		int xDist = 0;
		int yDist = 0;
		
		for (String d : directions){
		//Get new direction
			if (d.charAt(0) == 'R') {	//Turn right
				direction = (direction - 1 + 4) % 4;
			}
			if (d.charAt(0) == 'L') {	//Turn left
				direction = (direction + 1 + 4) % 4;
			}
			currentDir = cardinalDirections[direction];
		//Add distance
			if (currentDir == "left") {
				xDist -= Integer.parseInt(d.substring(1));
			}
			else if (currentDir == "right") {
				xDist += Integer.parseInt(d.substring(1));
			}
			else if (currentDir == "down") {
				yDist -= Integer.parseInt(d.substring(1));
			}
			else if (currentDir == "up") {
				yDist += Integer.parseInt(d.substring(1));
			}
		}
		
		return Math.abs(yDist) + Math.abs(xDist);
	}
	
	//Part Two
	//Returns the distance of the first revisited location
	public static int findFirstRevisitedLocation(String[] directions) {
		ArrayList<Coordinates> visited = new ArrayList<Coordinates>(); //Not best data structure but it will work for Day 1
		int direction = 0;	//Start up
		String currentDir = cardinalDirections[direction];
		int xDist = 0;
		int yDist = 0;
		
		for (String d : directions){
			int xChange = 0;
			int yChange = 0;
			
		//Get new direction
			if (d.charAt(0) == 'R') {	//Turn right
				direction = (direction - 1 + 4) % 4;
			}
			if (d.charAt(0) == 'L') {	//Turn left
				direction = (direction + 1 + 4) % 4;
			}
			currentDir = cardinalDirections[direction];
			
		//Add distance
			if (currentDir == "left") {
				xChange -= Integer.parseInt(d.substring(1));
				while (xChange != 0) {
					xDist -= 1;
					for (Coordinates c : visited) {
						if (c.x == xDist && c.y == yDist) {
							return c.distance;	
						}
					}
					Coordinates location = new Coordinates(xDist, yDist, Math.abs(xDist) + Math.abs(yDist));
					visited.add(location);
					xChange += 1;
				}
			}
			else if (currentDir == "right") {
				xChange += Integer.parseInt(d.substring(1));
				while (xChange != 0) {
					xDist += 1;
					for (Coordinates c : visited) {
						if (c.x == xDist && c.y == yDist) {
							return c.distance;	
						}
					}
					Coordinates location = new Coordinates(xDist, yDist, Math.abs(xDist) + Math.abs(yDist));
					visited.add(location);
					xChange -= 1;
				}
			}
			else if (currentDir == "down") {
				yChange -= Integer.parseInt(d.substring(1));
				while (yChange != 0) {
					yDist -= 1;
					for (Coordinates c : visited) {
						if (c.x == xDist && c.y == yDist) {
							return c.distance;	
						}
					}
					Coordinates location = new Coordinates(xDist, yDist, Math.abs(xDist) + Math.abs(yDist));
					visited.add(location);
					yChange += 1;
				}
			}
			else if (currentDir == "up") {
				yChange += Integer.parseInt(d.substring(1));
				while (yChange != 0) {
					yDist += 1;
					for (Coordinates c : visited) {
						if (c.x == xDist && c.y == yDist) {
							return c.distance;	
						}
					}
					Coordinates location = new Coordinates(xDist, yDist, Math.abs(xDist) + Math.abs(yDist));
					visited.add(location);
					yChange -= 1;
				}
			}
		}
		return Math.abs(yDist) + Math.abs(xDist);
	}
	
	//Used as a data structure to hold the information for Part Two
	static class Coordinates {
		int x;
		int y;
		int distance;
		
		public Coordinates(int x, int y, int distance) {
			this.x = x;
			this.y = y;
			this.distance = distance;
		}
	}
	
}
