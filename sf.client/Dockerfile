# Use the official Node.js image to build and run the Angular application
FROM node:18

# Set the working directory
WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm install --no-audit --no-fund

# Copy the rest of the application code
COPY . .

# Expose port 8080
EXPOSE 8080

# Start the Angular application
CMD ["npm", "start", "--", "--host", "0.0.0.0", "--port", "8080"]