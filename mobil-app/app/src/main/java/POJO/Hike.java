package POJO;

import android.os.Parcelable;

import java.io.Serializable;
import java.util.List;

public class Hike implements Serializable{
    public String getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public List<HikeHelper> getStaff() {
        return staff;
    }

    public List<HikeCourse> getCourses() {
        return courses;
    }

    private List<HikeCourse> courses;

    private String id;
    private String name;
    List<HikeHelper> staff;

    public boolean isHelper(int hikerId) {
       for(HikeHelper h : staff) {
           if (h.getHikerId() == hikerId) {return  true;}
       }
       return false;
    }
}
