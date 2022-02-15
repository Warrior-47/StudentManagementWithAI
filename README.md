# StudentManagementWithAI
## Project Description
A university website to manage students. Uses an AI to solve a CSP, for Pre-advising scheduling. Students can see their enrolled class schedules and their gradesheet. They can also use a built in CGPA calculator. More details on the AI below.

## Login
![Login page](/wwwroot/images/project_images/login.PNG)

## Registration
![Login page](/wwwroot/images/project_images/register.PNG)
>**Note:** Only Admin/Faculty users can register students or other admin/faculties.
## Student Dashboard
Dashboard showing the currently logged in student's enrolled class schedule.
![Dashboard](/wwwroot/images/project_images/dashboardStudent.PNG)

## Student Gradesheet
Currently logged in student's gradesheet
![Student's gradesheet](/wwwroot/images/project_images/studentgradesheet.PNG)

## CGPA Calculator
Given credit hours and grade points earned in courses, calculates the CGPA.
![CGPA Calculator](/wwwroot/images/project_images/cgpacalculator.PNG)

## Pre Advisor AI
Students need to do pre-advising to take courses for next semester. During that time, they usually have a faculty preference. However, it can get hectic to find a suitable section of each course they want to take without the class times clashing with one another. The problem of finding a suitable clashless schedule can be modelled as a Constraint Satisfaction Problem (CSP). The AI solves this problem using heuristic based Backtracking search.
![AI UI](/wwwroot/images/project_images/pre-advisor.PNG)

The clashless schedule is shown in a table.
![AI result](/wwwroot/images/project_images/pre-advisor-result.PNG)