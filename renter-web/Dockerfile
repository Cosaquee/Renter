FROM node

WORKDIR /app

COPY package.json .
RUN yarn

COPY . ./
ENTRYPOINT ["yarn", "start"]
