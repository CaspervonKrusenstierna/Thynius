import { DarknessEffect } from "../../shared/components"
import { Header } from "../../shared/components";
import RegisterBox from "./components/registerbox/RegisterBox";

function RegisterPage() {
    return (
        <div className="HomePage-unfocus"><DarknessEffect></DarknessEffect><Header></Header><RegisterBox></RegisterBox></div>
    );
}

export default RegisterPage;