fn is_leap_year(year: i32) -> bool {
    return is_divisible_by(year, 4) &&  !(is_divisible_by(year, 100) && !is_divisible_by(year, 400));
}

fn is_divisible_by(num: i32, divisor: i32) -> bool {
    return num % divisor == 0;
}

#[cfg(test)]
mod tests {
    use super::*;
    use test_case::test_case;

    #[test_case(1997, false ; "when not divisible by 4")]
    #[test_case(1800, false ; "when divisible by 100 but not 400")]
    #[test_case(1996, true ; "when divisible by 4")]
    #[test_case(1600, true ; "when divisible by 400")]
    fn it_can_deterimine_if_leapyear(year: i32, expected_result: bool) {
        assert!(is_leap_year(year) == expected_result);
    }
}
