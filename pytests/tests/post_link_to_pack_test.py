def test_digit_is_digit():
    elements = ["357","451", "024"]
    for elem in elements:
        assert elem.isdigit(), elem + "Its not number"