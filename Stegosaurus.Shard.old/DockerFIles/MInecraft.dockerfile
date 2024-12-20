# Use the OpenJDK base image
FROM openjdk:21-jdk

# Set the working directory
WORKDIR /minecraft

# Expose the necessary ports (default: 25565)
EXPOSE 25565

# Download Minecraft server jar file
ADD https://piston-data.mojang.com/v1/objects/45810d238246d90e811d896f87b14695b7fb6839/server.jar /minecraft/server.jar

# Accept the EULA
RUN echo "eula=true" > eula.txt

# Run the Minecraft server
CMD ["java", "-Xmx1024M", "-Xms1024M", "-jar", "server.jar", "nogui"]
