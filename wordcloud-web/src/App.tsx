import { Component } from 'solid-js';
import { Routes, Route, A } from '@solidjs/router';
import { CommentsGrid } from './pages/comments-grid';
import { WordCounts } from './pages/wordcounts';

const App: Component = () => {
	return (
		<>
			<h1>Word Count App</h1>
			<nav>
				<A href='/comments'>Comments Grid</A>
				<A href='/wordcounts'>Word Counts</A>
			</nav>
			<Routes>
				<Route path='/comments' component={CommentsGrid} />
				<Route path='/wordcounts' component={WordCounts} />
				<Route path='/' component={CommentsGrid} />
			</Routes>
		</>
	)
};

export default App;