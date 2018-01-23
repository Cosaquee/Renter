FROM ruby:latest
MAINTAINER Karol Kozakowski <cosaquee@gmail.com>

ENV AWS_ACCESS_KEY_ID asd
ENV AWS_SECRET_ACCESS_KEY asd

COPY Gemfile /opt/renter-cover/
COPY Gemfile.lock /opt/renter-cover/

WORKDIR /opt/renter-cover
RUN bundle install

COPY . /opt/renter-cover

EXPOSE 4567
CMD ["ruby", "main.rb"]
