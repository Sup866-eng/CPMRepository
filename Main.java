import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        Triangle tr = new Triangle(sc.nextInt(), sc.nextInt(), sc.nextInt());
        System.out.println(tr.Square());
    }
}