require 'sinatra'
require 'sinatra/cross_origin'
require 'aws-sdk-s3'
require 'guid'
require 'json'

before do
   content_type :json
   headers 'Access-Control-Allow-Origin' => 'http://localhost:8080',
            'Access-Control-Allow-Methods' => ['OPTIONS', 'POST'],
            'Access-Control-Allow-Headers' => 'Content-Type'
end

access_key_id = ENV['AWS_ACCESS_KEY_ID']
secret_access_key = ENV['AWS_SECRET_ACCESS_KEY']

client = Aws::S3::Client.new(
  region: 'eu-central-1',
  access_key_id: access_key_id,
  secret_access_key: secret_access_key
)

post '/save_image' do

  @filename = params[:file][:filename]
  file = params[:file][:tempfile]
  extension = File.extname(@filename)

  g = Guid.new
  g_filename = "#{g.to_s}#{extension}"

  File.open("./public/#{@filename}", 'wb') do |f|
    f.write(file.read)
  end

  obj = client.put_object({
    acl: "public-read",
    bucket: 'cmsrental',
    key: g_filename,
    body: IO.read("./public/#{@filename}")
  })

  {
    object_url: "https://s3.eu-central-1.amazonaws.com/cmsrental/#{g_filename}",
    filename: @filename
  }.to_json
end
