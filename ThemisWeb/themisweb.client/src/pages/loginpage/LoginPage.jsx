import { DarknessEffect } from "../../shared/components"
import { Header } from "../../shared/components/index";
import LoginBox from "./components/loginbox/LoginBox";
function LoginPage() {
    return (
        <div className='HomePage-unfocus'><DarknessEffect></DarknessEffect><Header></Header><LoginBox></LoginBox></div>
  );
}

export default LoginPage;