import React, { JSX } from 'react';
import Card from '../Card/Card';

interface Props {}

const CardList : React.FC<Props> = (props: Props) : JSX.Element => {
  return (
    <div>
      <Card companyName={'Apple'} ticker={'AAPL'} price={110} info={'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Magni, officia!'}></Card>
      <Card companyName={'Microsoft'} ticker={'MSFT'} price={200} info={'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Magni, officia!'}></Card>
      <Card companyName={'Tesla'} ticker={'TSLA'} price={300} info={'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Magni, officia!'}></Card>
    </div>
  );
}

export default CardList;