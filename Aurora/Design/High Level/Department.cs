public class Department
{

}
public class Designation
{

}
public class DepartmentServices
{
    public List<Department> GetDepartments();

    public Department GetDepartment(int DepartmentID);




    public List<Designation> GetDesignations(int DepartmentID);

    public Designation GetDesignation(int DesignationID);
}
