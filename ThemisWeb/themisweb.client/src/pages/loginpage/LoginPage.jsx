import { DarknessEffect } from "../../shared/components"
import { Header } from "../../shared/components/index";
import MainContentContainer from "../../shared/components/maincontentcontainer/MainContentContainer";
import WaveBackgroundBox from "../../shared/components/wavebackgroundbox/WaveBackgroundBox";
import LoginBox from "./components/loginbox/LoginBox";
function LoginPage() {
    return (
        <>
            <WaveBackgroundBox></WaveBackgroundBox>
            <LoginBox></LoginBox>
            <div className='HomePage-unfocus'><DarknessEffect></DarknessEffect><Header></Header><MainContentContainer></MainContentContainer></div>
        </>
  );
}

export default LoginPage;