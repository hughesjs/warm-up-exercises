fn main() {
    can_count_items(&get_test_stack());
    can_peek_without_removing(&get_test_stack());
    can_pop_corectly(&get_test_stack());
    returns_none_when_empty(&get_test_stack());
}

fn get_test_stack() -> Stack<i32> {
    let mut stack: Stack<i32> = Stack::new();
    
    stack.push(42);
    stack.push(555);
    stack.push(666);

    return stack;
}

fn can_count_items(stack: &Stack<i32>){
    assert!(stack.length() == 3);
}

fn can_peek_without_removing(stack: &Stack<i32>){
    assert!(stack.peek().unwrap() == 666);
    assert!(stack.length() == 3);
}

fn can_pop_corectly(stack: &Stack<i32>){
    assert!(stack.pop().unwrap() == 666);
    assert!(stack.pop().unwrap() == 555);
    assert!(stack.pop().unwrap() == 42);
}

fn returns_none_when_empty(stack: &Stack<i32>){
    assert!(stack.pop().is_none());
}


struct Stack<T> {
    stack: Vec<T>
}

impl<T> Stack<T> {
    fn new() -> Self {
        Stack {stack: Vec::new()}
    }

    fn pop(&mut self) -> Option<T> {
        return self.stack.pop();
    }

    fn push(&mut self, item: T) {
        self.stack.push(item);
    }

    fn clear(&mut self){
        self.stack.clear();
    }

    fn length(&self) -> usize{
        return self.stack.len();
    }

    fn peek(&self) -> Option<&T> {
        return self.stack.last();
    }

    fn is_empty(&self) -> bool {
       return self.stack.is_empty();
    }
}
