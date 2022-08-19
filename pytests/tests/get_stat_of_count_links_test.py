import requests

from pytests.src.main_classes.response import Response
from pytests.src.config.configuration import SERVICE_URL
from pytests.src.schemas.get_stat_schema import GET_STAT_SCHEMA


def test_stat_of_count_links():
    r = requests.get(
        url=SERVICE_URL + 'api/v1/stat', verify=False)
    response = Response(r)
    response.assert_status_code(200).validate(GET_STAT_SCHEMA)
    #print(response.json())
