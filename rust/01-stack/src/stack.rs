
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


#[cfg(test)]
mod tests{

    use super::*;

    fn get_test_stack() -> Stack<i32> {
        let mut stack: Stack<i32> = Stack::new();
        
        stack.push(42);
        stack.push(555);
        stack.push(666);
    
        return stack;
    }
    
    #[test]
    fn can_count_items(){
        let stack: Stack<i32> = get_test_stack();
        assert!(stack.length() == 3);
    }
    
    #[test]
    fn can_peek_without_removing(){
        let stack: Stack<i32> = get_test_stack();
        assert!(*stack.peek().unwrap() == 666);
        assert!(stack.length() == 3);
    }
   
    #[test]
    fn can_pop_corectly(){
        let mut stack: Stack<i32> = get_test_stack();
        assert!(stack.pop().unwrap() == 666);
        assert!(stack.pop().unwrap() == 555);
        assert!(stack.pop().unwrap() == 42);
    }
    
    #[test]
    fn returns_none_when_empty(){
        let mut stack: Stack<i32> = Stack::new();
        assert!(stack.pop().is_none());
    }

    #[test]
    fn can_clear_stack(){
        let mut stack: Stack<i32> = get_test_stack();
        assert!(stack.length() == 3);
        stack.clear();
        assert!(stack.length() == 0);
    }

    #[test]
    fn can_establish_if_stack_is_empty()
    {
        let mut stack: Stack<i32> = Stack::new();
        assert!(stack.is_empty());
        stack.push(123);
        assert!(!stack.is_empty());
    }
}