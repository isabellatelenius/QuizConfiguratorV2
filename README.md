# Quiz Configurator

A desktop quiz application developed in C# using WPF and XAML as part of a .NET development course. 

## Features

- Create, edit and delete question packs.
- Add, edit and remove questions with four answer options. 
- Select the correct answer for each question.
- Configure question pack settings, including name, difficulty and time limit.
- Play quizzes with randomized questions and answer options. 
- Answer questions within a time limit and receive feedback. 
- View the final score after conmpleting a quiz. 
- Save and load question packs using JSON.
- Navigate using menus and keyboard shortcuts. 

## Technologies

- C#
- .NET
- WPF and XAML
- MVVM architecture
- Data binding and commands
- JSON serialization
- Asynchronous file operations

## Project Structure

- **Models** - Question and QuestionPack.
- **ViewModels** - Configuration logic and commands.
- **Views** - Configuration, menu and quiz interface. 
- **Dialogs** - Dialog windows for creating and configuring question packs.
- **Services** - Dialog handling. 
- **Commands** - RelayCommand implementation. 

## Data Storage

Question packs are saved locally in JSON format using the user's LocalAppData directory. File are read and written asynchronously. 

## About

This project was developed as an individual assignment for a .NET development course.