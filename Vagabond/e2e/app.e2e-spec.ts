import { VagabondPage } from './app.po';

describe('vagabond App', () => {
  let page: VagabondPage;

  beforeEach(() => {
    page = new VagabondPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
