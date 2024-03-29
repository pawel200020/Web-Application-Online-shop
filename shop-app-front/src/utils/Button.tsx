export default function Button(props: buttonProps){
    return <button className='btn btn-primary'>{props.children}</button>
}
interface  buttonProps{
    children: React.ReactNode;
    onClick?: void;
    type: "button" | "submit"}

Button.defaultProps={
    type:"button"
}
