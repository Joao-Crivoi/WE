package model;


/**
 * Atilio Almeida Costa
 * João Victor Crivoi Cesar Souza
 */


public class Salesman {
    private String name;
    private String city;
    private double commission;

    public Salesman() {
    }

  
    public Salesman(String name, String city, double commission) {
        this.name = name;
        this.city = city;
        this.commission = commission;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getCity() {
        return city;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public double getCommission() {
        return commission;
    }

    public void setCommission(double commission) {
        this.commission = commission;
    }
}