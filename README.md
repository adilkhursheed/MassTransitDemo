# MassTransit Demo
This repository is created for a training demo for getting started with Mass Transit. It mimics a dributed system in which we have multiple systems doing their tasks. Check Step 1 and Step 2 Consumer. 

Initially its created for Rabbit MQ and In Memory, we'll later add Azure Buss Service and Amazon SQL since it would be just a configuration change.

**Publisher:**
A Background host service is created which add a message every second.

**Consumer:**
Two different consumers are developed. 
**Step1 Consumer** will read every message and randomely forward to Step2 Consumer.
**Step2 Consumer** will only process messages triggered by Step1.

![image](https://github.com/adilkhursheed/MassTransitDemo/assets/37509821/ecd91972-de0c-4890-bc6f-d3c4170a6b83)


**HAPPY CODING**
