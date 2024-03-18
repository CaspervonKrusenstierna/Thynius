import { DarknessEffect } from "../../shared/components"
import { Header } from "../../shared/components";
import MainContentContainer from "../../shared/components/maincontentcontainer/MainContentContainer";
import WaveBackgroundBox from "../../shared/components/wavebackgroundbox/WaveBackgroundBox";
import RegisterBox from "./components/registerbox/RegisterBox";

function RegisterPage() {
    return (
        <>
         <WaveBackgroundBox></WaveBackgroundBox>
         <RegisterBox></RegisterBox>
         <div className='HomePage-unfocus'><DarknessEffect></DarknessEffect><Header></Header><MainContentContainer></MainContentContainer></div>
        </>
    );
}

export default RegisterPage;