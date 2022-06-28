fn convert_from_roman(roman: &str) -> i32 { 
    let mut iter = roman.chars().peekable();
    let mut acc: i32 = 0;
    while let Some(c) = iter.next() {
        acc += get_acc_value(c, *iter.peek().unwrap_or(&'∅'));
    }
    return acc;
}

fn get_acc_value(current: char, next: char) -> i32 {
    let current_i: i32 = convert_single_numeral(current);
    let next_i: i32 = convert_single_numeral(next);

    if current_i >= next_i
    {
        return current_i;
    }
    else
    {
        return -current_i;
    }
}


fn convert_single_numeral(roman: char) -> i32 {
    return match roman {
        'I' => 1,
        'V' => 5,
        'X' => 10,
        'L' => 50,
        'C' => 100,
        'D' => 500,
        'M' => 1000,
        _ => 0,
    };
}

#[cfg(test)]
mod tests {
    use super::*;
    use test_case::test_case;
    use roman;

    #[test_case("I", 1    ; "when single digit I")]
    #[test_case("V", 5    ; "when single digit V")]
    #[test_case("X", 10   ; "when single digit X")]
    #[test_case("L", 50   ; "when single digit L")]
    #[test_case("C", 100  ; "when single digit C")]
    #[test_case("D", 500  ; "when single digit D")]
    #[test_case("M", 1000 ; "when single digit M")]
    #[test_case("II", 2   ; "when compound II")]
    #[test_case("III", 3  ; "when compound III")]
    #[test_case("XX", 20  ; "when compound XX")]
    #[test_case("XXX", 30 ; "when compound XXX")]
    #[test_case("XII", 12 ; "when compound XII")]
    #[test_case("CX", 110 ; "when compound CX")]
    #[test_case("MMXXII", 2022 ; "when compound MMXXII")]
    #[test_case("IV", 4 ; "when compound IV")]
    #[test_case("IX", 9 ; "when compound IX")]
    #[test_case("XIV", 14 ; "when compound XIV")]
    #[test_case("XIX", 19 ; "when compound XIX")]
    #[test_case("CM", 900 ; "when compound CM")]
    #[test_case("MMCD", 2400 ; "when compound MMCD")]
    fn it_can_convert_from_roman_numerals(roman: &str, val: i32) {
        assert!(convert_from_roman(roman) == val);
    }

    #[test]
    fn it_can_convert_every_roman_numeral_to_3999() {
        for i in 1..4000
        {
            let testRoman: &str = &roman::to(i).unwrap(); // Known-good crate
            let res: i32 = convert_from_roman(testRoman);
            assert!(res == i)
        }
    }
}
