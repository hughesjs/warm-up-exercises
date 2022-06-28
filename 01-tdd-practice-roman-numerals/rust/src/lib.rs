fn covert_from_roman(roman: &str) -> i32 {
    let mut acc: i32 = 0;
    for numeral in roman.chars() {
        acc += convert_single_numeral(numeral);
    }
    return acc;
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
    fn it_can_convert_from_roman_numerals(roman: &str, val: i32) {
        assert!(covert_from_roman(roman) == val);
    }
}
