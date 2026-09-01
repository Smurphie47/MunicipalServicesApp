# Municipal Services Application: Report Issues

## Overview

Municipal Services Application is a C# .NET Framework Windows Forms application developed for a South African municipal services scenario. It provides a user friendly way for residents to report municipal issues and submit the relevant details.

Part 1 implements **Report Issues**. **Local Events and Announcements** and **Service Request Status** are displayed on the main menu but remain disabled because they are intended for later development.

## Implemented Requirements

- Main menu presenting all three required municipal service tasks.
- Report Issues enabled and functional.
- Local Events and Announcements visible but disabled.
- Service Request Status visible but disabled.
- Location input using a `TextBox`.
- Category selection using a `ComboBox`.
- Detailed issue description using a `RichTextBox`.
- Image or document selection using `OpenFileDialog`.
- Submit Report and Back to Main Menu buttons.
- Required field validation and user feedback using `MessageBox`.
- Dynamic progress bar and encouraging messages as the engagement feature.
- In memory storage of multiple reports using `List<IssueReport>`.
- Sequential report numbers and a default `Submitted` status.
- Automatic form clearing after successful submission.

## User Engagement Feature

The application uses a dynamic progress bar and encouraging messages to guide residents through the reporting process. Progress increases when the required location, category and description fields are completed. At 100%, the application confirms that the report is ready to submit. This provides immediate feedback and encourages the user to complete the process.

## Data Structure

Reported issues are organised in a generic `List<IssueReport>` contained in the static `ReportRepository` class. Each report stores its ID, location, category, description, optional attachment path, submission date and status.

The list retains multiple reports while the application is running. The information is cleared when the application closes because permanent database storage is outside the scope of Part 1.

## Requirements to Compile and Run

- Windows 10 or Windows 11
- Visual Studio with the **.NET desktop development** workload
- .NET Framework 4.8 development tools and targeting pack

## How to Compile

1. Download or clone this repository.
2. Open `MunicipalServicesApp.slnx` in Visual Studio.
3. Allow Visual Studio to load the solution.
4. Select **Build > Rebuild Solution**.
5. Confirm that the Output window reports a successful build with no failed projects.

## How to Run

1. Open the solution in Visual Studio.
2. Press `F5`, or select the green **Start** button.
3. Wait for the Municipal Connect main menu to open.

## How to Use the Application

1. Select **Report Issues** on the main menu.
2. Enter the location of the municipal issue.
3. Select the appropriate issue category.
4. Enter a detailed description.
5. Optionally select **Attach File** and choose an image or document.
6. Check that the progress bar reaches 100%.
7. Select **Submit Report**.
8. Correct any missing information identified by a warning message.
9. After a valid submission, note the report number and `Submitted` status.
10. Select **Back to Main Menu** to return to the opening screen.

## Supported Attachments

- JPG and JPEG images
- PNG images
- GIF images
- PDF documents
- Microsoft Word documents (`.doc` and `.docx`)

The selected file path is recorded as part of the report. The file is not copied into permanent application storage.

## Project Structure

```text
MunicipalServicesApp/
|-- MunicipalServicesApp.slnx
|-- README.md
`-- MunicipalServicesApp/
    |-- MainMenuForm.cs
    |-- MainMenuForm.Designer.cs
    |-- ReportIssueForm.cs
    |-- ReportIssueForm.Designer.cs
    |-- IssueReport.cs
    |-- ReportRepository.cs
    |-- Program.cs
    |-- App.config
    `-- MunicipalServicesApp.csproj
```

## Main Source Files

| File | Purpose |
|---|---|
| `MainMenuForm.cs` | Presents the three tasks and opens the Report Issues form. |
| `ReportIssueForm.cs` | Handles input, progress, attachments, validation, submission and navigation. |
| `IssueReport.cs` | Defines the information stored for one reported issue. |
| `ReportRepository.cs` | Manages the shared `List<IssueReport>` and sequential report IDs. |

## Testing Completed

- Application compiles and runs successfully.
- Main menu opens on startup.
- Report Issues opens the correct form.
- The other two tasks remain disabled.
- Missing required fields produce warning messages.
- Required fields update the progress bar and messages.
- `OpenFileDialog` allows an image or document to be selected.
- Selected filename is displayed on the form.
- Valid submission produces a report number and status.
- Form clears after successful submission.
- A second valid report receives report number `2`, confirming multiple reports are stored.
- Back button returns to the main menu.

## Current Scope and Limitations

- Only Report Issues is implemented in Part 1.
- Reports are stored only for the current application session.
- Attachments are referenced by file path and are not copied to permanent storage.
- Local Events and Announcements will be implemented later.
- Service Request Status will be implemented later.

## Repository

https://github.com/Smurphie47/MunicipalServicesApp

## Author

**Dumisani Mvelase**
