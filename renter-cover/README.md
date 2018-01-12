# Renter Cover
Microservice to upload books and movies covers to S3

# Setup

* install Ruby
* install dependencies (`bundle install`)
* create `./public` folder
* export `AWS_SECRET_ACCESS_KEY` and `AWS_ACCESS_KEY_ID`
* run `ruby main.rb`

Service will be hosted on `localhost:4567`. If you want to upload book cover make `POST` request to `/save_image` path.

# TODO

* change path
* add movie cover upload
* Dockerize it
