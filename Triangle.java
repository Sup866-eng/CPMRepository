public class Triangle {
    private int aside;
    private int bside;
    private int cside;

    public Triangle(int aside, int bside, int cside){
        this.aside = aside;
        this.bside = bside;
        this.cside = cside;
    }

    public int Square(){
        int square = 0;
        int p = (aside + bside + cside)/2;
        try{
            square = (int) Math.sqrt(p*(p-aside)*(p-bside)*(p-cside));
        }
        catch(ArithmeticException e){
            System.out.println("Такого треугольника не существует!");
        }
        return square;
    }
}
