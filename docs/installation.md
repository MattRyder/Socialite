# Installing LifeCMS

LifeCMS is available on Windows, Mac OS X or Linux, using a technology called [Docker](https://docs.docker.com/get-docker). You don't need to have a deep understanding of Docker to install LifeCMS, just a few minutes of your time.

## Versioning

You can find the latest version of LifeCMS on [Docker Hub](https://hub.docker.com/u/lifecms). The default tag `latest` will always track the most recent stable release, to use a specific version, alter the `image` property inside the `docker-compose.yml` file.

For example:
```diff
identity.api:
-   image: lifecms_identity.api:latest
+   image: lifecms_identity.api:1.1
```

## Running the LifeCMS system

### Step 1: Download this repository

Once Docker is installed and running, enter the following commands into a terminal to download the repository:

```bash
git clone https://github.com/MattRyder/LifeCMS.git

cd LifeCMS/
```

### Step 2: Configure the system

LifeCMS can be branded in a particular way in order to fit in with your existing
brand, or as one product as part of an offering you may be providing to a
customer.

You can use the following environment variables as a base, but you should change
them to something more appropriate for your use case.

To learn more about what options are available for you to customise, take a look
at the "[Environment Variables](../ARCHITECTURE.md#environment-variables)"
section of LifeCMS' architecture document.

```
export LIFECMS_CONTENTCREATION_EMAIL_FROM_EMAIL_ADDRESS="no-reply@lifecms.localhost"
export LIFECMS_CONTENTCREATION_EMAIL_FROM_DISPLAY_NAME="no-reply"
export LIFECMS_EVENTBUS_QUEUENAME="email.lifecms.queue"
export LIFECMS_IDENTITY_EMAIL_FROM_EMAIL_ADDRESS="no-reply@lifcms.localhost"
export LIFECMS_RABBITMQ_USERNAME="rabbitmq"
export LIFECMS_RABBITMQ_PASSWORD="rabbitmq"
export LIFECMS_ORIGIN_CONTENTCREATION="https://contentcreation.lifcms.localhost"
export LIFECMS_ORIGIN_WEBSPA="https://lifcms.localhost"
export LIFECMS_VHOST_CONTENTCREATION="contentcreation.lifcms.localhost"
export LIFECMS_VHOST_IDENTITY="identity.lifcms.localhost"
export LIFECMS_VHOST_WEBSPA="lifcms.localhost"
export LIFECMS_VHOST_EMAIL="email.lifcms.localhost"
export LIFECMS_EMAIL_PROVIDER="Smtp"
export LIFECMS_EMAIL_SMTP_HOST="smtp.lifcms.localhost"
export LIFECMS_EMAIL_SMTP_PORT="1025"
export LIFECMS_IDENTITY_SERVER_AUTHORITY="https://identity.lifecms.localhost"
```

### Step 3: Start the system

Once the system has been configured, you can start all required services by running the following command:

```bash
docker-compose up --detach
```

This single command will:

 - Start a MySQL database (for storing data)
 - Start the RabbitMQ event bus (for inter-service communication)
 - Run any pending migrations for each service
 - Starts the front-end application on http://lifecms.localhost

Navigate to http://lifecms.localhost and you'll see the system.
