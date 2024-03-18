
import { Header } from "../../shared/components/index"
import MainContentContainer from "../../shared/components/maincontentcontainer/MainContentContainer"
import Footer from "../../shared/components/footer/Footer"
import WaveBackgroundBox from "../../shared/components/wavebackgroundbox/WaveBackgroundBox"
const HomePage = () => {
  return (
      <>
          <WaveBackgroundBox></WaveBackgroundBox>
          <Header></Header>  
          <MainContentContainer></MainContentContainer>
          <Footer></Footer>
      </>
  )
}

export default HomePage