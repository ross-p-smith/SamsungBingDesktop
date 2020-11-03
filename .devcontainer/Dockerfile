# See here for image contents: https://github.com/microsoft/vscode-dev-containers/tree/v0.140.1/containers/ubuntu/.devcontainer/base.Dockerfile

ARG VARIANT="2.1"
FROM mcr.microsoft.com/vscode/devcontainers/dotnetcore:${VARIANT}

# Install needed packages, yarn, nvm and setup non-root user. Use a separate RUN statement to add your own dependencies.
ARG USERNAME=node
ARG USER_UID=1001
ARG USER_GID=$USER_UID
ARG UPDATE_RC="true"
ENV JAVA_HOME=/usr/local/share/openJDK/jdk-12.0.2
ENV NVM_DIR=/usr/local/share/nvm
ENV NVM_SYMLINK_CURRENT=true \ 
    PATH=${NVM_DIR}/current/bin:${PATH}
COPY library-scripts/*.sh /tmp/library-scripts/

RUN apt-get update \
    && apt-get -y install --no-install-recommends pciutils 2>&1 \
    # Install common packages, non-root user, update yarn and install nvm
    && bash /tmp/library-scripts/common-debian.sh "${INSTALL_ZSH}" "${USERNAME}" "${USER_UID}" "${USER_GID}" "${UPGRADE_PACKAGES}" \
    # Install yarn, nvm
    && rm -rf /opt/yarn-* /usr/local/bin/yarn /usr/local/bin/yarnpkg \
    && bash /tmp/library-scripts/node-debian.sh "${NVM_DIR}" "none" "${USERNAME}" \
    # Clean up
    && apt-get autoremove -y && apt-get clean -y && rm -rf /var/lib/apt/lists/* /root/.gnupg /tmp/library-scripts

RUN curl -o /tmp/openjdk-12.0.2_linux-x64_bin.tar.gz \
    https://download.java.net/java/GA/jdk12.0.2/e482c34c86bd4bf8b56c0b35558996b9/10/GPL/openjdk-12.0.2_linux-x64_bin.tar.gz

RUN curl -o /tmp/openjfx-11.0.2_linux-x64_bin-sdk.zip \
    https://download2.gluonhq.com/openjfx/11.0.2/openjfx-11.0.2_linux-x64_bin-sdk.zip

RUN bash -c ". $NVM_DIR/nvm.sh && nvm install --lts"

RUN mkdir -p ${JAVA_HOME} \
    && tar xzvf /tmp/openjdk-12.0.2_linux-x64_bin.tar.gz -C /usr/local/share/openJDK \
    && unzip /tmp/openjfx-11.0.2_linux-x64_bin-sdk.zip -d /tmp \
    && cp -arf /tmp/javafx-sdk-11.0.2/lib/* ${JAVA_HOME}/lib/ \
    && sudo update-alternatives --install /usr/bin/java java ${JAVA_HOME}/bin/java 20000 \
    && sudo update-alternatives --install /usr/bin/javac javac ${JAVA_HOME}/bin/javac 20000

RUN curl -o /tmp/web-cli_Tizen_Studio_4.0_ubuntu-64.bin \
    http://download.tizen.org/sdk/Installer/tizen-studio_4.0/web-cli_Tizen_Studio_4.0_ubuntu-64.bin?
    
RUN chmod +x /tmp/web-cli_Tizen_Studio_4.0_ubuntu-64.bin \
    && mkdir -p /usr/local/share/tizen-studio

#RUN /tmp/web-cli_Tizen_Studio_4.0_ubuntu-64.bin --accept-license /usr/local/share/tizen-studio

#RUN curl -o csharp-1.21.12.vsix \
#    https://github.com/OmniSharp/omnisharp-vscode/releases/download/v1.21.12/csharp-1.21.12.vsix

