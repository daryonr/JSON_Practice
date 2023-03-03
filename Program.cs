using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using UpdatedProject.Models;
using System.Text;
using System;

string employeeJsonData = File.ReadAllText("Employees.json");
string newJsonString = employeeJsonData;
var employees = JsonSerializer.Deserialize<List<Employee>>(employeeJsonData);


if (employees != null)
{
_start:
    Console.WriteLine("1. Display Employees\n" + "2. Manage Employees\n" + "3. Export as JSON\n" + "4. Exit\n" + "Please Enter the Corresponding Number: ");
_promptloop:

    int intTemp = Convert.ToInt32(Console.ReadLine());

    if (intTemp == 1)
    {
        foreach (var employee in employees)
        {
            Console.WriteLine($"{employee.FirstName} {employee.LastName}; Employee ID: {employee.EmployeeID}");
        }

        Console.WriteLine("There are {0} total employees", employees.Count);
        goto _start;
    }

    else if (intTemp == 2)
    {
        Console.WriteLine("1. Remove Employee\n2. Edit Employee\n3. Add Employee\nPlease Enter the Corresponding Number: ");
        int userInput = Convert.ToInt32(Console.ReadLine());
        if (userInput == 1)
        {
            Console.WriteLine("Enter Employee ID:");

        _employIDprompt:

            string? userID = Console.ReadLine();
            var employeeToRemove = employees.Find(e => e.EmployeeID == userID);

            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);
                newJsonString = JsonSerializer.Serialize(employees);
                goto _start;
            }
            else
            {
                Console.WriteLine("Enter Valid Employee ID:\n");
                goto _employIDprompt;
            }
        }
        else if (userInput == 2)
        {
        _editprompt:
            Console.WriteLine("Enter Employee ID you want to edit:");
            string? idToEdit = Console.ReadLine();
            var employeeToEdit = employees.Find(e => e.EmployeeID == idToEdit);
            if (employeeToEdit == null)
            {
                Console.WriteLine("No person found with that ID.");
                goto _editprompt;
            }

            Console.Write("Enter the updated first name for this employee: ");
            string? newFirstName = Console.ReadLine();
            Console.Write("Enter the updated last name for this employee: ");
            string? newLastName = Console.ReadLine();
            Console.Write("Enter the updated email for this employee: ");
            string? newEmail = Console.ReadLine();
            Console.Write("Enter the updated number for this employee: ");
            string? newNumber = Console.ReadLine();

            employeeToEdit.FirstName = newFirstName;
            employeeToEdit.LastName = newLastName;
            employeeToEdit.Email = newEmail;
            employeeToEdit.Number = newNumber;

            newJsonString = JsonSerializer.Serialize(employees);

            goto _start;
        }
        else if (userInput == 3)
        {
            Console.Write("Enter the new ID for this employee: ");
            string? newID = Console.ReadLine();
            Console.Write("Enter the new first name for this employee: ");
            string? newFirstName = Console.ReadLine();
            Console.Write("Enter the new last name for this employee: ");
            string? newLastName = Console.ReadLine();
            Console.Write("Enter the new email for this employee: ");
            string? newEmail = Console.ReadLine();
            Console.Write("Enter the new number for this employee: ");
            string? newNumber = Console.ReadLine();

            // Add the new person to the collection
            employees.Add(new Employee { EmployeeID = newID, FirstName = newFirstName, LastName = newLastName, Email = newEmail, Number = newNumber });

            // Serialize the updated collection back into a JSON string
            newJsonString = JsonSerializer.Serialize(employees);

            goto _start;
        }
    }
    else if (intTemp == 3){
        string filePath = @"C:\updated_employees.json";

        Console.WriteLine(newJsonString);
        Console.WriteLine(File.ReadAllText(filePath));
    }
    else if (intTemp == 4){
        Environment.Exit(0);
    }
    else
    {
        Console.WriteLine("Please Enter a Valid Number:");
        goto _promptloop;
    }
}