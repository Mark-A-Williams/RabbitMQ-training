wt -d "C:\Projects\Training\RabbitMQ-training\MessageSender" --title "Sender" PowerShell.exe -NoExit -Command "dotnet watch run" `; `
split-pane -d "C:\Projects\Training\RabbitMQ-training\MessageReceiver" --title "Receiver1" Powershell.exe -NoExit -Command "dotnet watch run" `; `
split-pane -d "C:\Projects\Training\RabbitMQ-training\MessageReceiver" --title "Receiver2" Powershell.exe -NoExit -Command "dotnet watch run"
