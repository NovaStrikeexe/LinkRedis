from urllib import response
import requests

from configuration import SERVICE_URL


def post_link_to_pack():
    response = requests.post(url=SERVICE_URL + '/api/v1/link/pack/?link=23')
    #print("Test_Started")
