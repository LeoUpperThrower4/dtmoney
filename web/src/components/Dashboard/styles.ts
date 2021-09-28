import styled from "styled-components";

export const DashboardContainer = styled.div`
  transform: translateY(-50%);

  width: 100%;

  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 2rem;

  div:nth-child(3) {
    color: white;
    background-color: var(--green);

    p:nth-child(2) {
      color: white;
    }
  }
`

export const Block = styled.div`
  border-radius: .5rem;

  padding: 2rem;

  background-color: white;

  display: flex;
  flex-direction: column;
  gap: 1rem;
  
  p:first-child {
    display: flex;
    justify-content: space-between;
    align-items: center;

    svg, 
    img {
      height: 2rem;
      width: 2rem;
    }
  }

  p:nth-child(2) {
    font-size: 2.25rem;
    font-weight: bold;
    color: var(--titulos);
  }
`