using System;
using System.Collections.Generic;

namespace _Homework
{
	public class School
	{
		private const int FEE_PER_CREDIT = 1000;
		private List<Student> students;
		private List<Course> courses;
		private List<CourseToStudent> courseToStudents;

		public School()
		{
			this.students = new List<Student>();
			this.courses = new List<Course>();
			this.courseToStudents = new List<CourseToStudent>();
		}

		public void addCourse(string name)
		{
            //done by Chris
            var newCourse = new Course(name);
            courses.Add(newCourse);
		}

		public void addStudent(string name, int creditLimited)
		{
            //done by Chris
            var newStudent = new Student(name, creditLimited);
            students.Add(newStudent);
		}

		public void enrollStudentToCourse(int courseIndex, int studentIndex)
		{
			if (checkStudentCanEnroll(students[studentIndex], courses[courseIndex]))
			{
                //done by Chris
                var courseToStudent = new CourseToStudent(courses[courseIndex], students[studentIndex]);
                courseToStudents.Add(courseToStudent);
			}
			else
			{
				Console.WriteLine("Student " + students[studentIndex].name + "can not enroll");
			}
		}

		private bool checkStudentCanEnroll(Student student, Course course)
		{
			//Check course max student 
			return (checkCourseCanTakeStudent(course) && checkStudentCanTakeCourse(student, course));

		}

		private bool checkCourseCanTakeStudent(Course course)
		{
			List<Student> studentsInCourse = getStudentsFromCourse(course);
			return (studentsInCourse.Count < course.maxStudent);
		}

		private bool checkStudentCanTakeCourse(Student student, Course courseWillEnrolled)
		{
			List<Course> courseStudentTaking = getCourseFromStudent(student);
			int maxCredit = student.creditLimited;
			int studentCredit = 0;
			foreach (Course course in courseStudentTaking)
			{
				studentCredit = studentCredit + course.credit;
			}
			return (studentCredit + courseWillEnrolled.credit) <= maxCredit;
		}

		
        public void printStudent()
		{
            //done by Chris
            foreach(var s in students){
                Console.WriteLine(students.IndexOf(s) + 1 + ", " + s);
            }
		}
		
        public void printCourse()
		{
            //done by Chris
            foreach(var c in courses){
                Console.WriteLine(courses.IndexOf(c)+1+", "+c);
            }
		}

		public void removeStudent(int studentIdx)
		{
			Student selectedStudent = students[studentIdx];
			foreach (CourseToStudent courseTostudent in courseToStudents.ToArray())
			{
				if (courseTostudent.student == selectedStudent)
				{
					courseToStudents.Remove(courseTostudent);
				}
			}
			students.Remove(selectedStudent);
		}

		public void removeCourse(int courseIdx)
		{
			Course selectedCourse = courses[courseIdx];
			foreach (CourseToStudent courseTostudent in courseToStudents.ToArray())
			{
				if (courseTostudent.course == selectedCourse)
				{
					courseToStudents.Remove(courseTostudent);

				}
			}
			courses.Remove(selectedCourse); ;
		}
       
		private List<Student> getStudentsFromCourse(Course course)
		{
            //done by Chris
            List<Student> enrolledStudent = new List<Student>();
            foreach(var record in courseToStudents){
                if(record.course.Equals(course)){
                    enrolledStudent.Add(record.student);
                }
            }

            return enrolledStudent;
		}

		private List<Course> getCourseFromStudent(Student student)
		{
            //done by Chris
            List<Course> courseFromStudents = new List<Course>();
            foreach(var record in courseToStudents){
                if(record.student.Equals(student)){
                    courseFromStudents.Add(record.course);
                }
            }
            return courseFromStudents;
		}

		public void printCourseWithCourseIndex(int courseIndex)
		{
			Course selectedCourse = courses[courseIndex];
			int count = 0;
			for (int i = 0; i < courseToStudents.Count; i++)
			{
				if (courseToStudents[i].course == selectedCourse)
				{
					Console.WriteLine((i + 1) + "." + courseToStudents[i].course.className + " - " + courseToStudents[i].student.name);
					count++;
				}
			}
			Console.WriteLine("The total student of " + selectedCourse.className + " : " + count);
		}

		public void printCourseWithStudentIndex(int studentIdx)
		{
			Student selectedStudent = students[studentIdx];
			int count = 0;
			for (int i = 0; i < courseToStudents.Count; i++)
			{
				if (courseToStudents[i].student == selectedStudent)
				{
					Console.WriteLine((i + 1) + "." + courseToStudents[i].course.className + " - " + courseToStudents[i].student.name);
					count++;
				}
			}
			Console.WriteLine("The total course of " + selectedStudent.name + " enrolled is " + count);

		}

		public void printCourseToStudent()
		{
			for (int i = 0; i < courseToStudents.Count; i++)
			{
				Console.WriteLine((i + 1) + "." + courseToStudents[i].course.className + " - " + courseToStudents[i].student.name);
			}
		}
	}
}
