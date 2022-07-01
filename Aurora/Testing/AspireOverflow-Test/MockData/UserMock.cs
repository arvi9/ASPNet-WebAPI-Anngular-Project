using System;
using System.Collections.Generic;
using AspireOverflow.Models;

namespace AspireOverflowTest
{
    static class UserMock
    {

        public static User GetInValidUser()
        {
            return new User();
        }
        public static User GetValidUser() => new User()
        {
            FullName = "Raghu",
            GenderId = 1,
            AceNumber = "ACE1234",
            EmailAddress = "raghu@aspiresys.com",
            Password = "Raghu@3721",
            DateOfBirth = new System.DateTime(),
            UserRoleId = 2,
            DesignationId = 1,
        };
        public static List<User> GetListOfUser()
        {
            return new List<User>()
            {
                 new User(){UserId=1,FullName="Raghu",GenderId=1,AceNumber="ACE1234",EmailAddress="raghu@aspiresys.com",Password="Raghu@3721",DateOfBirth=new System.DateTime(2000,09,23),IsReviewer=false,UserRoleId=2,DesignationId=1,},
                 new User(){UserId=2,FullName="sandhiya",GenderId=2,AceNumber="ACE1234",EmailAddress="sandhiya@aspiresys.com",Password="Sandhiya@3721",DateOfBirth=new System.DateTime(2000,09,23),IsReviewer=false,UserRoleId=2,DesignationId=1,},
                 new User(){UserId=3,FullName="Raghu",GenderId=1,AceNumber="ACE1234",EmailAddress="raghu@aspiresys.com",Password="Raghu@3721",DateOfBirth=new System.DateTime(2000,09,23),IsReviewer=false,UserRoleId=2,DesignationId=1,},
                 new User(){UserId=4,FullName="Sandhiya",GenderId=2,AceNumber="ACE1234",EmailAddress="sandhiya@aspiresys.com",Password="sandhiya@3721",DateOfBirth=new System.DateTime(2000,09,23),IsReviewer=false,UserRoleId=2,DesignationId=1,},
                 new User(){UserId=5,FullName="Raghu",GenderId=1,AceNumber="ACE1234",EmailAddress="raghu@aspiresys.com",Password="Raghu@3721",DateOfBirth=new System.DateTime(2000,09,23),IsReviewer=false,UserRoleId=2,DesignationId=1,},
                 new User(){UserId=6,FullName="Raghu",GenderId=1,AceNumber="ACE1234",EmailAddress="raghu@aspiresys.com",Password="Raghu@3721",DateOfBirth=new System.DateTime(2000,09,23),IsReviewer=false,UserRoleId=2,DesignationId=1,},

            };
        }
        public static List<Department> GetListOfDepartments()
        {
            return new List<Department>()
            {
                new Department(){DepartmentId=1,DepartmentName="Dotnet"},
                new Department(){DepartmentId=2,DepartmentName="LAMP"},
                new Department(){DepartmentId=3,DepartmentName="Oracle"},
                new Department(){DepartmentId=4,DepartmentName="BFS"},

            };
        }
        public static List<Designation> GetListOfDesignation()
        {
            return new List<Designation>()
            {
                new Designation(){DepartmentId=1,DesignationId=1,DesignationName="ProjectManager"},
                new Designation(){DepartmentId=1,DesignationId=2,DesignationName="ModuleLead"},
                new Designation(){DepartmentId=3,DesignationId=3,DesignationName="TeamLead"},
                new Designation(){DepartmentId=4,DesignationId=4,DesignationName="ProjectLead"},
            };
        }
        public static List<Gender> GetListOfGender()
        {
            return new List<Gender>()
            {
                new Gender(){GenderId=1,Name="Male"},
                new Gender(){GenderId=2,Name="Female"}
            };
        }
        public static List<User> GetListOfUsersForSeeding()
        {
            return new List<User>(){
                new User(){UserId=1,FullName="Mani Maran",AceNumber="ACE9898",EmailAddress="Mani.Venkat@aspiresys.com",Password="88888888",VerifyStatusID=1},
                 new User(){UserId=2,FullName="Sriram",AceNumber="ACE9898",EmailAddress="Mani.Venkat@aspiresys.com",Password="898989898",VerifyStatusID=2},
       
        };
        }
        public static List<Department> GetListOfDepartmentsForSeeding()
        {
            return new List<Department>(){
            new Department(){DepartmentId=1,DepartmentName="Dotnet"},
            new Department(){DepartmentId=2,DepartmentName="LAMP"},
            new Department(){DepartmentId=3,DepartmentName="Oracle"},
            new Department(){DepartmentId=4,DepartmentName="BFS"},
            
        };

    }
    public static List<Designation> GetListOfDesignationForSeeding()
        {
            return new List<Designation>(){
            new Designation(){DepartmentId=1,DesignationName="Project manager"},
            new Designation(){DepartmentId=2,DesignationName="Module Lead"},
            new Designation(){DepartmentId=3,DesignationName="Team Lead"},
            new Designation(){DepartmentId=4,DesignationName="Project Manager"},
        };
    }
    public static List<Gender> GetListOfGenderForSeeding()
        {
            return new List<Gender>()
            {
                new Gender(){GenderId=1,Name="Male"},
                new Gender(){GenderId=2,Name="Female"},
            };
        }
}
}