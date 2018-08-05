package POJO;

import java.io.Serializable;

public class HikeCourse implements Serializable{
    CheckPoint[] checkPoints;
    int id;
    String name;

    public CheckPoint[] getCheckPoints() {
        return checkPoints;
    }

    public int getId() {
        return id;
    }

    public String getName() {
        return name;
    }
}
