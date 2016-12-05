import java.security.*;

public class Day5 {
	
	public static void main(String[] args) {
		String input = "abbhdwsy";
		
		partOne(input);
		partTwo(input);
	}
	
	public static void partOne(String input) {
		String hash;
		int count = 0;
		int i = 0;
		String answer = "";
		while (count < 8) {
			hash = MD5(input + i);
			if (hash.startsWith("00000")) {
				answer += hash.charAt(5);
				count += 1;
			}
			i += 1;
		}
		
		System.out.println("Part One: " + answer);
	}

	public static void partTwo(String input) {
		char[] answer = {'g','g','g','g','g','g','g','g'};
		String hash;
		int i = 0;
		boolean atLeastOneG = true;
		while (atLeastOneG) {
			hash = MD5(input + i);
			if (hash.startsWith("00000")) {
				int index = Character.getNumericValue(hash.charAt(5));
				if (index < 8 && answer[index] == 'g') {
					answer[index] = hash.charAt(6); 
				}
				atLeastOneG = checkAnswer(answer);
			}
			i += 1;
		}
		
		String solution = "";
		for (char c: answer) {
			solution += c;
		}
		
		System.out.println("Part Two: " + solution);
	}
	
	//Found at http://stackoverflow.com/questions/415953/how-can-i-generate-an-md5-hash
	public static String MD5(String md5) {
		   try {
		        java.security.MessageDigest md = java.security.MessageDigest.getInstance("MD5");
		        byte[] array = md.digest(md5.getBytes());
		        StringBuffer sb = new StringBuffer();
		        for (int i = 0; i < array.length; ++i) {
		          sb.append(Integer.toHexString((array[i] & 0xFF) | 0x100).substring(1,3));
		       }
		        return sb.toString();
		    } catch (java.security.NoSuchAlgorithmException e) {
		    }
		    return null;
		}
	
	public static boolean checkAnswer(char[] answer) {
		for (char c : answer) {
			if (c == 'g') {
				return true;
			}
		}
		return false;
	}
	
}
