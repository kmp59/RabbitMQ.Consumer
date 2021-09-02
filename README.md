<div align="center">

  # RabbitMQ.Consumer

</div>

<h4>RabbitMQ Consumer app that creates the Quorum Queue and attaches to it, in order to listen to the incoming messages.</h4>

1. Creates connection with local instance of RabbitMQ
2. Creates the Queue if already doesn't exists
3. Receives messages from the Queue and parse it back to string
