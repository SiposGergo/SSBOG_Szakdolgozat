package POJO;

import java.io.Serializable;

public class CheckPoint implements Serializable{
    public int getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    private int id;
    private String name;
}
