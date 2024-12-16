 
     
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/notificationHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();
    
    connection.on('newMessage', (sender, messageText) => {
        console.log(`${sender}:${messageText}`);

        //const newMessage = document.createElement('li');
        //newMessage.appendChild(document.createTextNode(`${sender}:${messageText}`));
        //messages.appendChild(newMessage);
    });

    connection.start()
        .then(() => console.log('connected!'))
    .catch(console.error);

connection.invoke('SendMessage', "a");

    //messageForm.addEventListener('submit', ev => {
    //    ev.preventDefault();
    //    const message = messageBox.value;
    //    connection.invoke('SendMessage', message);
    //    messageBox.value = '';
    //}); 
 