
import { WaveBackgroundBox, Header, MainContentContainer, Footer } from "../../shared/components/homepage"
import { Outlet } from "react-router-dom"
const HomePage = () => {
  return (
      <div className="flex-column">
          <WaveBackgroundBox></WaveBackgroundBox>
          <Header></Header>  
          <MainContentContainer></MainContentContainer>
          <Footer></Footer>
          <Outlet></Outlet>
      </div>
  )
}

export default HomePage